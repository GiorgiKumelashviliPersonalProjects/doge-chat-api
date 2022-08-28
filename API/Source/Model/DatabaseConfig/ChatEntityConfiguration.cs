using API.Source.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Source.Model.DatabaseConfig;

public class ChatEntityConfiguration: IEntityTypeConfiguration<ChatMessage>
{
    public void Configure(EntityTypeBuilder<ChatMessage> builder)
    {
        builder.ToTable("ChatMessages");
        
        builder
            .HasOne(e => e.Sender)
            .WithMany(e => e.SentChatMessages)
            .HasForeignKey(e => e.SenderId)
            .IsRequired();
        
        builder
            .HasOne(e => e.Receiver)
            .WithMany(e => e.ReceivedChatMessages)
            .HasForeignKey(e => e.ReceiverId)
            .IsRequired();
        
        builder.Property(e => e.CreatedAt)
            .HasDefaultValueSql("NOW()")
            .ValueGeneratedOnAdd();
    }
}